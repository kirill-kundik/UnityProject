namespace Enemies
{
	public class GreenOrc : Orc {
		
		protected override void OnRabbitEntered()
		{
			OrcMode = Mode.GoToRabbit;
		}
		
	}
}
