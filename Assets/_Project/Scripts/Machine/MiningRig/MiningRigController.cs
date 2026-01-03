using UnityEngine;
using System.Collections;
public class MiningRigController : MonoBehaviour
{
    public MiningRigModel miningRigModel;
    public MiningRigStateMachine miningRigStateMachine;
    public IEnumerator MiningRoutine()
    {
        while(miningRigStateMachine.miningRigState == MachineState.Active)
        {
            GoldManager.Instance.AddGold(miningRigModel.output);
            
            yield return new WaitForSeconds(miningRigModel.leadTime);
        }
        yield break;
    }
}
