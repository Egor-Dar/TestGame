namespace ScreenSystem.Scripts
{
    public class ScreenState
    {
        private ScreenStateObject _currentScreen;

        public void SetScreen(ScreenStateObject screen)
        {
            if (_currentScreen != null)
            {
                _currentScreen.SetActive(false);
            }
            _currentScreen = screen.SetActive(true);
        }
    }
}
