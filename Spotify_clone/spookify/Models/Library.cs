using spookify.Models;

public class Library : BaseEntity
{
    public int LibraryId {get;set;}
    public int UserId {get;set;}
    public User User {get;set;}
    public int AlbumId {get;set;}
    public Album Album {get;set;}

}