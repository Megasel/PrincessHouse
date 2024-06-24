
namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // Тестовые сохранения для демо сцены
        // Можно удалить этот код, но тогда удалите и демо (папка Example)
        public int money = 1;                       // Можно задать полям значения по умолчанию
        public string newPlayerName = "Hello!";
        public bool[] openLevels = new bool[3];

        public int Onetime = 0;
        public float health1;
        public float health2;
        public int rateus;
        public int isSoundOn;
        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {
            isSoundOn = 1;
        }
    }
}
