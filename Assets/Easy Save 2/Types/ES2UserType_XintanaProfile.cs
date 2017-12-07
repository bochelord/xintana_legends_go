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
		writer.Write(data.tokens);
		writer.Write(data.audioEnabled);

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
		data.tokens = reader.Read<System.Int32>();
		data.audioEnabled = reader.Read<System.Boolean>();

	}
	
	/* ! Don't modify anything below this line ! */
	public ES2UserType_XintanaProfile():base(typeof(XintanaProfile)){}
}