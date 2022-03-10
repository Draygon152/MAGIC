// Written by Kevin Chao
//Modified by Marc Hagoriles

public class OptionsMenu : Menu<OptionsMenu>
{
    public void VideoOptionsPressed()
    {
        VideoOptions.Open();
    }


    public void SoundOptionsPressed()
    {
        SoundOptions.Open();
    }


    public void ReturnPressed()
    {
        Close();
    }
}