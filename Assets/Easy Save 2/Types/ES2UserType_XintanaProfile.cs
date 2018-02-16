using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ES2UserType_XintanaProfile : ES2Type
{
	public override void Write(object obj, ES2Writer writer)
	{
		XintanaProfile data = (XintanaProfile)obj;
		// Add your writer.Write calls here.
		writer.Write(data.gameVersion);
		writer.Write(data.profileID);
		writer.Write(data.highscore);
		writer.Write(data.gems);
		writer.Write(data.timesKilledByZazuc);
		writer.Write(data.timesKilledByMakula);
		writer.Write(data.timesKilledByKogi);
		writer.Write(data.timesKilledByBlackKnight);
		writer.Write(data.level);
		writer.Write(data.experience);
		writer.Write(data.weaponEquipped);
		writer.Write(data.adsViewed);
		writer.Write(data.adsSkipped);
		writer.Write(data.audioEnabled);
		writer.Write(data.extraLife);
		writer.Write(data.noAds);
		writer.Write(data.doubleScore);
		writer.Write(data.firstTimePlayed);
		writer.Write(data.timeStamp);
		writer.Write(data.sharedScoreTimes);

	}
	
	public override object Read(ES2Reader reader)
	{
		XintanaProfile data = GetOrCreate<XintanaProfile>();
		Read(reader, data);
		return data;
	}

	public override void Read(ES2Reader reader, object c)
	{
		XintanaProfile data = (XintanaProfile)c;
		// Add your reader.Read calls here to read the data into the object.
		data.gameVersion = reader.Read<System.String>();
		data.profileID = reader.Read<System.Int32>();
		data.highscore = reader.Read<System.Int32>();
		data.gems = reader.Read<System.Int32>();
		data.timesKilledByZazuc = reader.Read<System.Int32>();
		data.timesKilledByMakula = reader.Read<System.Int32>();
		data.timesKilledByKogi = reader.Read<System.Int32>();
		data.timesKilledByBlackKnight = reader.Read<System.Int32>();
		data.level = reader.Read<System.Int32>();
		data.experience = reader.Read<System.Single>();
		data.weaponEquipped = reader.Read<weapon>();
		data.adsViewed = reader.Read<System.Int32>();
		data.adsSkipped = reader.Read<System.Int32>();
		data.audioEnabled = reader.Read<System.Boolean>();
		data.extraLife = reader.Read<System.Boolean>();
		data.noAds = reader.Read<System.Boolean>();
		data.doubleScore = reader.Read<System.Boolean>();
		data.firstTimePlayed = reader.Read<System.Boolean>();
		data.timeStamp = reader.Read<System.DateTime>();
		data.sharedScoreTimes = reader.Read<System.Int32>();

	}
	
	/* ! Don't modify anything below this line ! */
	public ES2UserType_XintanaProfile():base(typeof(XintanaProfile)){}
}