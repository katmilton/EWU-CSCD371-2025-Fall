using System;
namespace CanHazFunny;


/* TODO:
 * As you are handling the Output Interface, feel free to modify 
or delete this as you see fit. I've just kept this here for testing.
Do delete this comment when you do.
 */
public interface IOutput
{
	void WriteLine(string message);
}

public class Jester
{

	private readonly IOutput _output;
	private readonly IJokeService _jokeService;
	private const int MaxAttempts = 10;

	public Jester(IOutput output, IJokeService jokeService)
	{
		_output = output ?? throw new ArgumentNullException(nameof(output));
		_jokeService = jokeService ?? throw new ArgumentNullException(nameof(jokeService));

	}

	public void TellJoke()
	{
		for (int i = 0; i < MaxAttempts; i++)
		{
			var joke = _jokeService.GetJoke();
			if (!joke.Contains("Chuck Norris", StringComparison.OrdinalIgnoreCase))
			{
				_output.WriteLine(joke);
				return;
			}
		}
		_output.WriteLine("Failed to find an acceptable joke. Try again!");
	}
}
