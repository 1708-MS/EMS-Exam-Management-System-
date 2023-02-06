namespace CustomiseIdentity.Repository.iRepository
{
    public interface IUnitOfWork
    {
        ISubjectRepository Subject { get; }
    }
}
