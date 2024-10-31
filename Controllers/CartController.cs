using DoAnPhanMem.Data;
using DoAnPhanMem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class CartController : Controller
{
    private readonly dbSportStoreContext _db;

    public CartController(dbSportStoreContext db)
    {
        _db = db;
    }

    
}
