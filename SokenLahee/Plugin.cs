using System.IO;
using System.Media;
using System.Reflection;
using Dalamud.Plugin;

namespace SokenLahee
{
    public class Plugin : IDalamudPlugin
    {
        public string Name => "Soken Lahee";
        
        private DalamudPluginInterface _pi;
        private SoundPlayer  _player;

        public void Initialize(DalamudPluginInterface pluginInterface)
        {
            _pi = pluginInterface;
            _pi.ClientState.TerritoryChanged += OnTerritoryChanged;
            
            var _localDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            _player = new SoundPlayer { SoundLocation = Path.Combine(_localDir, "soken.wav") };

            OnTerritoryChanged(this, _pi.ClientState.TerritoryType);
        }
        
        public void Dispose()
        {
            _player.Dispose();
            _pi.ClientState.TerritoryChanged -= OnTerritoryChanged;
            _pi.Dispose();
        }

        private void OnTerritoryChanged(object sender, ushort territory)
        {
            //PluginLog.Log($"changed: {territory}");

            if (territory == 817) //817 is Rak'tika
            {
                _player.PlayLooping();
            }
            else
            {
                _player.Stop();
            }
        }
    }
}
