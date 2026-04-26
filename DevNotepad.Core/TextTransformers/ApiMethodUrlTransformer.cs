using System.Collections.Generic;
using System.Linq;

namespace DevNotepad.Core.TextTransformers
{
    [TextTransformer("Api method URL", "bc889037-8bad-474c-bf79-fb7c47d61107")]
    public class ApiMethodUrlTransformer : LineTransformer
    {
        private static readonly IDictionary<string, int> ServiceNames = new Dictionary<string, int>
        {
            { "AuthorizationService", 9027 },
            { "BindService", 9038 },
            { "DictionariesService", 9010 },
            { "CommandsService", 9029 },
            { "OrdersService", 9013 },
            { "ProductsService", 9012 },
            { "ProductsUpdateService", 9019 },
            { "EmployeesService", 9011 },
            { "GeolocationService", 9017 },
            { "SchedulerService", 9018 },
            { "NotificationsService", 9028 },
            { "SyncService", 9031 },
            { "ImageService", 9032 },
            { "SenderService", 9033 },
            { "PluginsService", 9034 },
            { "PublicApi", 9902 },
            { "YandexEdaService", 9039 },
            { "TransportService", 9025 },
            { "UocServicePublic", 9904 },
            { "UocServiceInternal", 9022 },
        };

        protected override string TransformLine(string line)
        {
            var tokens = line.Split('.');
            if (tokens.Length < 4)
                return line;

            if (tokens[0] != "iikoTransport")
                return line;

            var serviceName = tokens[1];
            var methodName = tokens[^1];
            var controllerToken = tokens[^2];

            if (serviceName == "UocService")
                serviceName = tokens.Contains("Public")
                    ? "UocServicePublic"
                    : "UocServiceInternal";

            if (!ServiceNames.TryGetValue(serviceName, out var port))
                return line;

            var controllerSuffix = "Controller";
            if (!controllerToken.EndsWith(controllerSuffix))
                return line;
            var controllerName = controllerToken[..^controllerSuffix.Length];

            if (serviceName == "PublicApi")
            {
                if (controllerName == "Internal")
                    return $"http://localhost:{port}/api/internal/{methodName}";
                if (controllerName == "RmsSettings")
                    return $"http://localhost:{port}/api/internal/rmsSettings/{methodName}";
            }

            return $"http://localhost:{port}/api/{controllerName}/{methodName}";
        }
    }
}