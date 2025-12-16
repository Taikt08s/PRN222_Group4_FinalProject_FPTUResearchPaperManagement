using System;
using System.Threading.Tasks;
using DataAccessLayer;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace PRN222_Group4_FinalProject_FPTUResearchPaperManagement.Pages.Instructor.Submission
{
    [Authorize(Roles = "Instructor,GraduationProjectEvaluationCommitteeMember")]
    public class DownloadFileModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly StorageClient _storage;
        private const string BucketName = "fpturesearchpapermanagement.firebasestorage.app";

        public DownloadFileModel(AppDbContext context, StorageClient storage)
        {
            _context = context;
            _storage = storage;
        }

        public async Task<IActionResult> OnGetAsync(int fileId)
        {
            if (fileId <= 0) return BadRequest();

            var file = await _context.SubmissionFiles
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.Id == fileId);

            if (file == null) return NotFound();

            // Parse object name from Firebase URL (same pattern used elsewhere)
            string objectName;
            try
            {
                var uri = new Uri(file.Firebase_Url);
                var path = uri.AbsolutePath.TrimStart('/'); // "bucket/object/..."
                var prefix = BucketName + "/";
                if (!path.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                    return BadRequest("Invalid Firebase URL for stored object.");

                var encodedObject = path.Substring(prefix.Length);
                objectName = Uri.UnescapeDataString(encodedObject);
            }
            catch (Exception)
            {
                return BadRequest("Invalid Firebase URL.");
            }

            try
            {
                // Get object metadata to set content type
                var obj = await _storage.GetObjectAsync(BucketName, objectName);

                Response.ContentType = obj.ContentType ?? "application/octet-stream";
                var safeFileName = Uri.EscapeDataString(file.File_Name ?? "download");
                Response.Headers["Content-Disposition"] = $"attachment; filename=\"{file.File_Name}\"; filename*=UTF-8''{safeFileName}";

                // Stream object directly to response
                await _storage.DownloadObjectAsync(BucketName, objectName, Response.Body);
                await Response.Body.FlushAsync();
                return new EmptyResult();
            }
            catch (Google.GoogleApiException gex) when (gex.Error != null && gex.Error.Code == 403)
            {
                // Access denied from GCS -> service account likely missing permission
                return StatusCode(403, "Access to storage object denied. Ensure service account has storage.objects.get on the bucket.");
            }
            catch (Google.GoogleApiException gex) when (gex.Error != null && gex.Error.Code == 404)
            {
                return NotFound("Object not found in storage.");
            }
            catch (Exception ex)
            {
                // generic error
                return StatusCode(500, "Download failed: " + ex.Message);
            }
        }
    }
}