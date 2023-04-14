using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceGenerator.Model.ValidationErrors
{
    public class ErrorsKeeper
    {
        private Dictionary<string, List<string>> _errros;
        public bool IsValid { get => _errros.Count == 0; }

        public ErrorsKeeper()
        {
            _errros = new Dictionary<string, List<string>>();
        }

        public void AddErrors(string Type, string Message)
        {
            if (_errros.ContainsKey(Type))
                _errros[Type].Add(Message);
            else
                _errros.Add(Type, new List<string>() { Message });
        }

        public void ClearErros() => _errros.Clear();
    }
}
