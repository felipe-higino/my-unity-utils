using System;
using System.Threading.Tasks;

public interface ISceneTransition
{
    Task LowerCourtine();
    Task LiftCourtine();
}
