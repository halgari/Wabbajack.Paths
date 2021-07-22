using System;

namespace Wabbajack.Paths
{
    public struct AbsolutePath : IPath, IComparable<AbsolutePath>, IEquatable<AbsolutePath>
    {
        
        #if Windows
        private static char _separator = '\\';
        #else
        private static char _separator = '/';
        #endif
        
        #if Windows
        private static string _prefixString = "";
        #else
        private static stirng _prefixString = "/";
        #endif
        
        
        private readonly string[] _parts;
        private AbsolutePath(string[] parts)
        {
            _parts = parts;
        }

        private static char[] _stringSplits = {'/', '\\'};
        private static AbsolutePath Create(string path)
        {
            var parts = path.Split(_stringSplits, StringSplitOptions.RemoveEmptyEntries);
            return new AbsolutePath(parts);
        }

        public AbsolutePath Parent()
        {
            if (_parts.Length <= 1)
                throw new PathException($"Path {this} does not have a parent folder");
            var newParts = new string[_parts.Length - 1];
            Array.Copy(_parts, newParts, newParts.Length);
            return new (newParts);
        }

        public static explicit operator AbsolutePath(string input)
        {
            return Create(input);
        }

        public override string ToString()
        {
            return _prefixString + string.Join(_separator, _parts);
        }

        public int CompareTo(AbsolutePath other)
        {
            return string.Compare(ToString(), other.ToString(), StringComparison.Ordinal);
        }

        public bool Equals(AbsolutePath other)
        {
            if (_parts.Length != other._parts.Length) return false;
            for (var idx = 0; idx < _parts.Length; idx++)
            {
                if (_parts[idx] != other._parts[idx])
                    return false;
            }
            return true;
        }
    }
    
}