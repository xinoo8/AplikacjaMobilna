namespace AplikacjaMobilna;
public partial class MainPage : ContentPage
{
    private List<Song> songs = new();
    private int currentIndex = 0;

    public MainPage()
    {
        InitializeComponent();
        LoadSongs();
        ShowSong(currentIndex);
    }
    private void LoadSongs()
    {
        var file = FileSystem.OpenAppPackageFileAsync("music.txt").Result;
        using var reader = new StreamReader(file);
        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();
            var parts = line.Split(';');
            if (parts.Length == 4)
            {
                songs.Add(new Song
                {
                    Author = parts[0],
                    Title = parts[1],
                    Duration = parts[2],
                    Image = parts[3].Trim()
                });
            }
        }
    }
    private void ShowSong(int index)
    {
        var song = songs[index];
        titleLabel.Text = song.Title;
        authorLabel.Text = song.Author;
        coverImage.Source = song.Image;
        durationLabel.Text = song.Duration;
        durationSlider.Value = 0;
    }
    private void OnNextClicked(object sender, EventArgs e)
    {
        currentIndex++;
        if (currentIndex >= songs.Count)
            currentIndex = 0;
        ShowSong(currentIndex);
    }
    private void OnPreviousClicked(object sender, EventArgs e)
    {
        currentIndex--;
        if (currentIndex < 0)
            currentIndex = songs.Count - 1;
        ShowSong(currentIndex);
    }
    private void OnPlayClicked(object sender, EventArgs e)
    {
        DisplayAlert("Play", $"Odtwarzanie: {songs[currentIndex].Title}", "OK");
    }
}
public class Song
{
    public required string Author { get; set; }
    public required string Title { get; set; }
    public required string Duration { get; set; }
    public required string Image { get; set; }
}