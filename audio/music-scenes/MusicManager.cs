using Godot;

public partial class MusicManager : Node {
	private AudioStreamPlayer MusicPlayer;

	public override void _Ready() {
		MusicPlayer = new AudioStreamPlayer();
		AddChild(MusicPlayer);
	}

	public void Play(AudioStream music) {
		if (MusicPlayer.Stream == music && MusicPlayer.Playing)
			return;
		// MusicPlayer.Stream = music;
		// MusicPlayer.Play();
	}

	public void Stop() {
		// MusicPlayer.Stop();
	}
}
