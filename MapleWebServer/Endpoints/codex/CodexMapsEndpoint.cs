using Maple2Storage.Types.Metadata;
using MapleServer2.Data.Static;

namespace MapleWebServer.Endpoints;

public static class CodexMapsEndpoint
{
    public static IResult Get()
    {
        List<MapMetadata> metadata = MapMetadataStorage.GetAll().ToList();
        if (metadata is null)
        {
            return Results.BadRequest();
        }

        return Results.Text(Newtonsoft.Json.JsonConvert.SerializeObject(metadata));
    }
}
