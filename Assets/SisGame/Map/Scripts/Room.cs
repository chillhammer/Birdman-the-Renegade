using UnityEngine;
using System.Collections.Generic;

//Basically graph node
//RoomType will allow for custom rooms to be generated
[System.Serializable]
public class Room
{
	public Rect rect;
	public HashSet<Room> connected;
	[SerializeField] public Room generatedFrom; //meta
	public RoomType roomType = RoomType.Normal;

	#region Constructors
	private Room() { }

	public Room(Vector2 position, Vector2 size, Room sourceRoom = null)
	{
		rect = new Rect(position, size);
		connected = new HashSet<Room>();
		generatedFrom = sourceRoom;
		ConnectToSource();
	}

	public Room(int x, int y, int width, int height, Room sourceRoom = null)
	{
		rect = new Rect(x, y, width, height);
		connected = new HashSet<Room>();
		generatedFrom = sourceRoom;
		ConnectToSource();
	}

	private void ConnectToSource()
	{
		if (generatedFrom == null)
			return;
		generatedFrom.connected.Add(this);
		connected.Add(generatedFrom);
	}
	#endregion

	public override int GetHashCode()
	{
		return rect.GetHashCode();
	}
}

public enum RoomType { Normal }