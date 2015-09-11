﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml.Linq;

namespace Sampler
{
    class Configuration
    {
        private readonly Dictionary<int, Player> _mappings;

        public string Name
        {
            get; private set; }

        private Configuration(string name)
        {
            _mappings = new Dictionary<int, Player>();
            Name = name;
        }
         
        public static Configuration Parse(XElement element)
        {
            Configuration config  = new Configuration(element.Attribute("name").Value);

            foreach (var child in element.Descendants("Sound"))
            {
                var keyCode = int.Parse(child.Attribute("keyCode").Value);
                var soundUri = new Uri(child.Attribute("path").Value, UriKind.Relative);
                Player p = new Player(soundUri);
                config._mappings.Add(keyCode, p);
            }

            return config;
        }

        public Player GetSound(int keyCode)
        {
            if (_mappings.ContainsKey(keyCode))
            {
                return _mappings[keyCode];
            }
            else
            {
                return null;
            }
        }
    }
}
