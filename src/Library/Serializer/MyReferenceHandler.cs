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
        public override ReferenceResolver CreateResolver() => rootedResolver;
        public void Reset() => rootedResolver = new MyReferenceResolver();
    }

    class MyReferenceResolver : ReferenceResolver
    {
        private readonly Dictionary<string, object> referenceIdToObjectMap = new();
        private readonly Dictionary<object, string> objectToReferenceIdMap = new(ReferenceEqualityComparer.Instance);
        private uint referenceCount;

        public override void AddReference(string referenceId, object value)
        {
            if (!referenceIdToObjectMap.TryAdd(referenceId, value))
            {
                throw new JsonException();
            }
        }

        public override string GetReference(object value, out bool alreadyExists)
        {
            if (objectToReferenceIdMap.TryGetValue(value.ToString(), out string referenceId))
            {
                alreadyExists = true;
            }
            else
            {
                referenceCount++;
                referenceId = referenceCount.ToString();
                objectToReferenceIdMap.Add(value, referenceId);
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