using System.ComponentModel.DataAnnotations;

namespace FileApprovalSystem.ViewModels.Files
{
    public class CreateFileViewModel
    {
        [Required]
        public string FileNumber { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public DateTime FileDate { get; set; }

        public string ResponsibleEmployee { get; set; }

        public IFormFile Attachment { get; set; }
    }
}
