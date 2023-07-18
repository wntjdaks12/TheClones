public interface IActorState
{
    public void OnIdle(ActorObject actorObject);
    public void OnMove(ActorObject actorObject);
    public void OnDeath(ActorObject actorObject);
    public void OnHit(ActorObject actorObject);
}
