using System;
using System.Linq;
using FsCheck.Xunit;
using Xunit;

namespace Wabbajack.Paths.Test
{

    public class PathTests
    {
        [Fact]
        public void CanParsePaths()
        {
            Assert.Equal(((AbsolutePath)@"c:\foo\bar").ToString(), ((AbsolutePath)@"c:\foo\bar").ToString());
        }
        [Fact]
        public void CanGetParentPath()
        {
            Assert.Equal(((AbsolutePath)@"c:\foo").ToString(), ((AbsolutePath)@"c:\foo\bar").Parent().ToString());
        }
        
        [Fact]
        public void ParentOfTopLevelPathThrows()
        {
            Assert.Throws<PathException>(()=>((AbsolutePath)@"c:\").Parent().ToString());
        }
        
        [Fact]
        public void PathsAreEquatable()
        {
            Assert.Equal((AbsolutePath)@"c:\foo", (AbsolutePath)@"c:\foo");
            Assert.NotEqual((AbsolutePath)@"c:\foo", (AbsolutePath)@"c:\bar");
            Assert.NotEqual((AbsolutePath)@"c:\foo\bar", (AbsolutePath)@"c:\foo");
        }

        [Fact]
        public void PathsAreComparable()
        {
            var data = new[]
            {
                (AbsolutePath) @"c:\a",
                (AbsolutePath) @"c:\b\c",
                (AbsolutePath) @"c:\d\e\f",
                (AbsolutePath) @"c:\b",
            };
            var data2 = data.OrderBy(a => a).ToArray();
            
            var data3 = new[]
            {
                (AbsolutePath) @"c:\a",
                (AbsolutePath) @"c:\b",
                (AbsolutePath) @"c:\b\c",
                (AbsolutePath) @"c:\d\e\f",
            };
            Assert.Equal(data3, data2);
            
        }
    }
}