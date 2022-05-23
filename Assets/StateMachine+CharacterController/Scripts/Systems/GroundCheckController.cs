using Zenject;

public class GroundCheckController : GameSystem
{
    [Inject(Id = IDConstants.GROUNDCHECK)] private readonly GroundCheckData _groundCheckData;
    [Inject(Id = IDConstants.GROUNDCHECK)] private readonly Sensor _sensor;

    public bool GroundCheck()
    {
        if (!_enabled) return false;

        if (_sensor.hit.distance <= _groundCheckData.GroundDistance) return true;

        return false;
    }
}
