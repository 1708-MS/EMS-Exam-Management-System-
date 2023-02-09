namespace CustomiseIdentity.Repository.iRepository
{
    public interface IUnitOfWork
    {
        ISubjectRepository Subject { get; }
        IExamPaperRepository ExamPaper { get; }
        IQuestionRepository Question { get; }
        void Save();
    }
}
