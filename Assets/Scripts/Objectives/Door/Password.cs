using UnityEngine;

namespace Password.Model
{
    public class Password // Generates a different password every time
    {
        private string _password;
        public Password()
        {
            GeneratePassword();
        }

        public void GeneratePassword()
        {
            int digit1 = Random.Range(0, 10);
            int digit2 = Random.Range(0, 10);
            int digit3 = Random.Range(0, 10);
            int digit4 = Random.Range(0, 10);

            _password = $"{digit1}{digit2}{digit3}{digit4}";
        }

        public string GetPassword()
        {
            return _password;
        }  
    }
}
