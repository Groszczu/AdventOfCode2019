
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day6
{
    public class SpaceObjectsTree
    {
        public SpaceObject CenterOfMass { get; }
        public int TotalOrbits => _objects.Sum(p => p.Value.TotalOrbits);
        private Dictionary<string, SpaceObject> _objects;

        public SpaceObjectsTree()
        {
            CenterOfMass = new SpaceObject(null);
            _objects = new Dictionary<string, SpaceObject> { { "COM", CenterOfMass } };
        }

        public void AddOrbiterFromString(string input)
        {
            var pattern = new Regex(@"^(?<name>(\w+))\)(?<orbiter>(\w+))$");
            if (!pattern.IsMatch(input))
            {
                throw new InvalidDataException("Given string doesn't match the pattern");
            }

            var match = pattern.Match(input);
            var parentName = match.Groups["name"].Value;
            var orbiterName = match.Groups["orbiter"].Value;

            if (!_objects.ContainsKey(parentName))
            {
                _objects.Add(parentName, new SpaceObject(null));
            }

            if (_objects.ContainsKey(orbiterName))
            {
                _objects[orbiterName].Parent = _objects[parentName];
            }
            else
            {
                AddOrbiter(orbiterName, _objects[parentName]);
            }
            
        }
        public void AddOrbiter(string name, SpaceObject parent)
        {
            _objects.Add(name, new SpaceObject(parent));
        }

        public int OrbitalTransfersBetween(string fromName, string toName)
        {
            var from = _objects[fromName];
            var to = _objects[toName];
            return from.OrbitalTransfersTo(to);
        }
    }
}