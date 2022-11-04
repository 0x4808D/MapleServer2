using Maple2Storage.Types.Metadata;
using MapleServer2.Data.Static;

namespace MapleWebServer.Endpoints;

public static class CodexNPCEndpoint
{
    public static IResult Get()
    {
        List<NpcMetadata> metadata = NpcMetadataStorage.GetAll().ToList();
        if (metadata is null)
        {
            return Results.BadRequest();
        }

        return Results.Text(Newtonsoft.Json.JsonConvert.SerializeObject(metadata));
    }
}
