using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "CharacterInitInstallSO", menuName = "Installers/CharacterInitInstallSO")]
public class CharacterInitInstallSO : ScriptableObjectInstaller<CharacterInitInstallSO>
{
    [SerializeField] private GroundCheckDataSO _groundCheck;

    public override void InstallBindings()
    {
        Container.Bind<GroundCheckData>().FromInstance(_groundCheck.Data).AsSingle().NonLazy();
        Container.Bind<ITickable>().To<GroundCheckController>().AsSingle().NonLazy();
        Container.Bind<CharacterStateData>().AsSingle().NonLazy();
    }
}
