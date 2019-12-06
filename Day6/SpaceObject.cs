using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day6
{
    public class SpaceObject
    {
        public SpaceObject Parent { get; set; }

        public int DirectOrbits => Parent == null ? 0 : 1;
        public int IndirectOrbits
        {
            get
            {
                var result = 0;
                for (var currentObject = Parent; currentObject != null; result++)
                {
                    currentObject = currentObject?.Parent;
                }

                return result;
            }
        }

        public int TotalOrbits => DirectOrbits + IndirectOrbits;

        public SpaceObject(SpaceObject parent)
        {
            Parent = parent;
        }

        public int OrbitalTransfersTo(SpaceObject other)
        {
            var parents = GetAllParents();
            var otherParents = other.GetAllParents();

            var firstCommonParent = parents.Intersect(otherParents).First();

            return Parent.NumberOfStepsTo(firstCommonParent) + other.Parent.NumberOfStepsTo(firstCommonParent);
        }
        private List<SpaceObject> GetAllParents()
        {
            var parents = new List<SpaceObject>();
            for (var currentObject = Parent; currentObject != null; currentObject = currentObject?.Parent)
            {
                parents.Add(currentObject);
            }

            return parents;
        }

        private int NumberOfStepsTo(SpaceObject other)
        {
            var result = 0;
            for (var currentObject = this; currentObject != other; result++)
            {
                if (currentObject == null)
                {
                    return -1;
                }
                currentObject = currentObject?.Parent;
            }

            return result;
        }
    }
}