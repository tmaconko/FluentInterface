using BalticAmadeus.FluentMdx;

namespace FluentInterface.Examples
{
    // ReSharper disable UnusedVariable
    // ReSharper disable UnusedMember.Global
    // ReSharper disable UnusedMember.Global

    public class QueryExamples
    {
        public void SimpleWay()
        {
            var axisSlicerMember = new MdxMember();
            axisSlicerMember.Titled("Dim Hierarchy", "Dim");
            
            var axisSlicer = new MdxTuple();
            axisSlicer.With(axisSlicerMember);

            var axis = new MdxAxis();
            axis.Titled(MdxAxisType.Columns);
            axis.AsNonEmpty();
            axis.WithSlicer(axisSlicer);

            var cube = new MdxCube();
            cube.Titled("Cube");

            var querySlicerMember = new MdxMember();
            querySlicerMember.Titled("Dim Hierarchy", "Dim", "Dim Key");
            querySlicerMember.WithValue("1");

            var querySlicer = new MdxTuple();
            querySlicer.With(querySlicerMember);
            
            var query = new MdxQuery();
            query.On(axis);
            query.From(cube);
            query.Where(querySlicer);
        }

        public void FluentWay()
        {
            var query = Mdx.Query()
                .On(Mdx.Axis(0).AsNonEmpty()
                    .WithSlicer(Mdx.Tuple()
                        .With(Mdx.Member("Dim Hierarchy", "Dim"))))
                .From(Mdx.Cube("Cube"))
                .Where(Mdx.Tuple()
                    .With(Mdx.Member("Dim Hierarchy", "Dim", "Dim Key")
                        .WithValue("1")));
        }

    }
}
