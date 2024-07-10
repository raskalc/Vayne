using HostMgd.ApplicationServices;
using Teigha.DatabaseServices;
using Teigha.Geometry;

namespace Vayne.Utils;

public class Draw
{
    public void DrawSphere(int r, int x, int y, int z)
    {
        var doc = Application.DocumentManager.MdiActiveDocument;
        var db = doc.Database;
        // Начинаем транзакцию
        var tr = db.TransactionManager.StartTransaction();

        // Получаем пространство модели для добавления объектов
        var bt = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForRead);
        var btr = (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);

        // Создаем новую сферу
        var sphere = new Solid3d();

        sphere.SetDatabaseDefaults();
        sphere.CreateSphere(r); 

        // Устанавливаем позицию сферы
        sphere.TransformBy(Matrix3d.Displacement(new Vector3d(x, y, z)));

        // Добавляем сферу в пространство модели
        btr.AppendEntity(sphere);
        tr.AddNewlyCreatedDBObject(sphere, true);


        // Коммитим транзакцию
        tr.Commit();
        doc.Editor.Regen();
    }

    public void DrawTriangle(int x1, int y1, int x2, int y2, int x3, int y3)
    {
        var doc = Application.DocumentManager.MdiActiveDocument;
        var db = doc.Database;

        // Начинаем транзакцию
        var tr = db.TransactionManager.StartTransaction();

        // Открываем пространство модели для записи
        var btr = (BlockTableRecord)tr.GetObject(db.CurrentSpaceId, OpenMode.ForWrite);

        // Создаем три точки треугольника
        var pt1 = new Point3d(x1, y1, 0);
        var pt2 = new Point3d(x2, y2, 0);
        var pt3 = new Point3d(x3, y3, 0);

        // Создаем полилинию, представляющую треугольник
        var polyline = new Polyline();

        polyline.AddVertexAt(0, new Point2d(pt1.X, pt1.Y), 0, 0, 0);
        polyline.AddVertexAt(1, new Point2d(pt2.X, pt2.Y), 0, 0, 0);
        polyline.AddVertexAt(2, new Point2d(pt3.X, pt3.Y), 0, 0, 0);
        polyline.Closed = true;

        // Добавляем полилинию в пространство модели
        btr.AppendEntity(polyline);
        tr.AddNewlyCreatedDBObject(polyline, true);


        // Подтверждаем изменения в транзакции
        tr.Commit();
    }
}