using Inventor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DesignAutomationInventorUtilities_Tests.Helpers
{
    class NameValueMapStub : NameValueMap
    {
        private Dictionary<string, object> map = new Dictionary<string, object>();

        public void Add(string Name, object Value)
        {
            throw new NotImplementedException();
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void Remove(object Index)
        {
            throw new NotImplementedException();
        }

        public void Insert(string Name, object Value, object TargetIndex, bool InsertBefore = true)
        {
            throw new NotImplementedException();
        }

        public object Value { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int Count => throw new NotImplementedException();

        public object Item => throw new NotImplementedException();

        public string Name => throw new NotImplementedException();

        public object get_Value(string Name)
        {
            return map[Name];
        }

        public void set_Value(string Name, object value)
        {
            map[Name] = value;
        }

        public object get_Item(object Index)
        {
            return null;
        }

        public string get_Name(int Index)
        {
            return null;
        }
    }
}
