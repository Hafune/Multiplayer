using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Services
{
    public static class MigrationService
    {
        public const ReleaseVersion ACTUAL_VERSION = ReleaseVersion.v04_07_25;
        private static List<ReleaseVersion> _versions = Enum.GetValues(typeof(ReleaseVersion)).Cast<ReleaseVersion>().ToList();

        public static T Run<T>(
            IDataService dataService,
            ISerializableService key,
            params IMigration[] migrations
        ) where T : new()
        {
            var dataVersion = dataService.GetDataVersion();

            if (dataVersion == ACTUAL_VERSION)
                return dataService.DeserializeData<T>(key);

            int index = -1;
            var migrationList = migrations.ToList();
            for (int versionIndex = _versions.IndexOf(dataVersion); versionIndex < _versions.Count; versionIndex++)
            {
                index = migrationList.FindIndex(m => m.From() == _versions[versionIndex]);
                if (index != -1)
                    break;
            }

            if (index < 0)
                return dataService.DeserializeData<T>(key);

            var data = migrations[index].ReadFromData(dataService, key);

            for (int i = index; i < migrations.Length; i++)
                data = migrations[i].Migrate(data);

            dataService.SetDirty(key, true);
            return (T)data;
        }
    }
}