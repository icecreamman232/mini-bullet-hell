
namespace JustGame.Scripts.Enemy
{
    public class BrainDecisionAlwaysTrue : BrainDecision
    {
        public override bool CheckDecision()
        {
            return true;
        }
    }  
}

