using System;
using System.Collections.Generic;
using System.IO;
using Raven.Imports.Newtonsoft.Json.Bson;
using Raven.Json.Linq;

namespace Benchmark {
  public static class Program {

    public static void Main(string[] args) {
      Environment.SetEnvironmentVariable("PATH", 
          Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]) + "/");

      Console.WriteLine(Environment.GetEnvironmentVariable("PATH"));
      var count = 20;
      using(var db = new Db(count)) {
        Populate(db, count);
        //DoUnStreamedTest(db, count);
        DoStreamedTest(db, count);
      }
    }

    public static void DoStreamedTest(Db db, int count) {
      List<Object> cached = new List<Object>();

      for(var i = 0 ; i < count ; i++) {
        var stream = new ReadStream(db, i);
        var reader = new BsonReader(stream);
        var obj = RavenJObject.ReadFrom(reader);
        cached.Add(obj);
        if(cached.Count > 5)
          cached.RemoveAt(0);
        Console.WriteLine("Loaded {0}", i);
      }
    }

    public static void DoUnStreamedTest(Db db, int count) {
      List<Object> cached = new List<Object>();

      for(var i = 0 ; i < count ; i++) {
        var size = db.GetSize(i);
        var lo = new Byte[size];
        db.Get(i, lo, 0, lo.Length);
        var stream = new MemoryStream(lo);
        var reader = new BsonReader(stream);
        var obj = RavenJObject.ReadFrom(reader);
        cached.Add(obj);
        if(cached.Count > 5)
          cached.RemoveAt(0);

        Console.WriteLine("Loaded {0}", i);
      }
    }
    
		public static void Populate(Db db, int count) {
			Console.WriteLine ("Generating object graph");
			var stream = new MemoryStream();
			var node = CreateTreeNode(3, 50, 0);
			BsonWriter writer = new BsonWriter(stream);
			var obj = RavenJObject.FromObject(node);
			obj.WriteTo(writer);
			writer.Flush();
			for(var i =0 ; i < count; i++) {
				Console.WriteLine ("Writing object with key: {0}, and {1} bytes", i, stream.Length);
        db.Put(i, stream.ToArray());
			}
		}

		public static TreeNode CreateTreeNode(int levels, int numberPerLevel, int c) {
			var node = new TreeNode();
			node.Name = String.Format ("levels{0}number{1}", levels, c);
			if(levels > 0) {
				List<TreeNode> children = new List<TreeNode>();
				for(var i =0 ; i < numberPerLevel; i++) {
					children.Add(CreateTreeNode(levels-1, numberPerLevel, c));
				}
				node.Nodes = children.ToArray();
			}
			return node;
		}
	}


	public class TreeNode 
	{
		public string Name { get; set; }
		public TreeNode[] Nodes { get; set;}
	}
}
