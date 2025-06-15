namespace JobBoard.Data.Models;

public class JobApplicants : BaseEntity
{
    public int JobId { get; set; }

    public int ApplicantId { get; set; }

    public virtual Job Job{ get; set; } = null!;

    public virtual Users Applicant { get; set; } = null!;
}
