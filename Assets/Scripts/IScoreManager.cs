public interface IScoreManager
{
    void Attach(IScoreObserver observer);
    void Detach(IScoreObserver observer);

    void Notify();
}
