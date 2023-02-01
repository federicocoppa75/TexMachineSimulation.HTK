using CncViewer.Connection.Interfaces.Links;
using System;
using System.Collections.Generic;
using System.Text;
using CncViewer.Connection.Interfaces.Enums;
using System.Linq;
using GZSoft.Tex.Controller.Interface;
using GZSoft.Tex.Controller;

namespace CncViewer.Connection.Bridge
{
    internal class VariablesSet
    {
        private IList<IVariable> _variables;

        public int[] Indexes { get; private set; }
        public Dictionary<int, int> IndexesMap { get; private set; } = new Dictionary<int, int>();

        private VariablesSet()
        {

        }

        public static VariablesSet Create(IList<IVariable> variables, VariableType type)
        {
            var vs = new VariablesSet();

            for (int i = 0; i < variables.Count; i++)
            {
                var v = variables[i]; 
                if ((v.VariableType == type) && (v.Index >= 0))
                {
                    vs.IndexesMap[variables[i].Index] = i;
                }
            }

            if (vs.IndexesMap.Count == 0) return null;

            vs.Indexes = vs.IndexesMap.Keys.OrderBy(i => i).ToArray();
            vs._variables = variables;

            return vs;
        }

        public void Read<T>(IReadOnlyVariableService<T> service)
        {
            var coll = service.Read(SubSystem.PLC, Indexes);

            if(coll != null)
            {
                foreach (var item in coll)
                {
                    var idx = IndexesMap[item.Key];
                    
                    _variables[idx].SetValue(item.Value);
                }
            }
        }
    }
}
