using System;
using System.Collections.Generic;
using GoingPostal.Entities;

namespace GoingPostal;

public class Level(int id)
{
    public int RoomId {get;} = id;


    public List<EntityBase> Entities {get;} = [];


    
}