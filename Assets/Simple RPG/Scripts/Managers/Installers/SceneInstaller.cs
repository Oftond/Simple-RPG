using Player.Movement;
using SimpleRPG.Services.Input;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        var PIS = FindFirstObjectByType<PlayerInputService>();
        Container.BindInstance(PIS).AsSingle();

        var PMS = FindFirstObjectByType<PlayerMovementSystem>();
        Container.BindInstance(PMS).AsSingle();
    }
}