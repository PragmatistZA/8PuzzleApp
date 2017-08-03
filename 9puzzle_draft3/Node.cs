using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _9puzzle_draft3
{
    public class Node<T>
    {
        // Private member-variables
        private T data;

        public Node(T data)
        {
            this.data = data;
        }

        public T Value
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
            }
        }

        public T getInfo()
        {
            return data;
        }
    }
}

