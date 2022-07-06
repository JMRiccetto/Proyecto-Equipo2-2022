using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NavalBattle
{
    public class MyReferenceHandler : ReferenceHandler
    {
        private static MyReferenceHandler instance;

        public static MyReferenceHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MyReferenceHandler();
                }

                return instance;
            }
        }

        public MyReferenceHandler() => Reset();
        private ReferenceResolver rootedResolver;
        public override ReferenceResolver CreateResolver() => this.rootedResolver;
        public void Reset() => this.rootedResolver = new MyReferenceResolver();
    }

    class MyReferenceResolver : ReferenceResolver
    {
        private readonly Dictionary<string, object> referenceIdToObjectMap = new ();
        private readonly Dictionary<object, string> objectToReferenceIdMap = new (ReferenceEqualityComparer.Instance);
        private uint referenceCount;

        public override void AddReference(string referenceId, object value)
        {
            if (!this.referenceIdToObjectMap.TryAdd(referenceId, value))
            {
                throw new JsonException();
            }
        }

        public override string GetReference(object value, out bool alreadyExists)
        {
            if (this.objectToReferenceIdMap.TryGetValue(value.ToString(), out string referenceId))
            {
                alreadyExists = true;
            }
            else
            {
                this.referenceCount++;
                referenceId = this.referenceCount.ToString();
                this.objectToReferenceIdMap.Add(value, referenceId);
                alreadyExists = false;
            }

            return referenceId;
        }

        public override object ResolveReference(string referenceId)
        {
            if (!this.referenceIdToObjectMap.TryGetValue(referenceId, out object value))
            {
                throw new JsonException();
            }

            return value;
        }
    }
}