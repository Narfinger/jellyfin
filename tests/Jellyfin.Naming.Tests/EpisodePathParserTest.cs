namespace Emby.Naming.TV
{
    using Emby.Naming.Common;
    using Xunit;

    public class EpisodePathParserTest
    {
        [Theory]
        [InlineData("/media/Foo/Foo-S01E01", "Foo", 1, 1)]
        [InlineData("/media/Foo - S04E011", "Foo", 4, 11)]
        [InlineData("/media/Foo/Foo s01x01", "Foo", 1, 1)]
        [InlineData("/media/foo/Foo.s01_e03", "Foo", 1, 3)]
        [InlineData("/media/foo/foo.ep01", "Foo", 1, 1)]
        [InlineData("/media/Foo/foo/0199", "foo", 1, 99)]
        [InlineData("/media/foo/Season 0003/s0003e02", "foo", 3, 2)]
        [InlineData("/media/Foo/Foo s01x03 - the bar of foo", "Foo", 1, 3)]
        [InlineData("/media/Foo/[Bar] Foo - 01 [baz]", "Foo", 1, 1)]
        [InlineData("/media/FooBar/[Baz] Foo Bar - 03 [baz][bbaz]", "Foo Bar", 1, 3)]
        [InlineData("/media/foo/[baz] Foo - 03 (baz)", "Foo", 1, 3)]
        public void ParseEpisodesCorrectly(string path, string name, int season, int episode)
        {
            NamingOptions o = new NamingOptions();
            EpisodePathParser p = new EpisodePathParser(o);
            var res = p.Parse(path, false);

            Assert.True(res.Success);
            Assert.Equal(name, res.SeriesName);
            Assert.Equal(season, res.SeasonNumber);
            Assert.Equal(episode, res.EpisodeNumber);
        }
    }
}