using Godot;
using System.Threading.Tasks;

public interface ICombat
{
	void ApplyDamageToPlayer();
	int RandomNumber(int maxNumber);
	void SetHealth(ProgressBar progressBar, int health, int maxHealth);
	void _Process(double delta);
	void _Ready();
}