namespace Sociam.Application.Bases
{
    public class Either<TLeft, TRight>
    {
        public TLeft? Left { get; }
        public TRight? Right { get; }
        public bool IsLeft { get; }

        private Either(TLeft left)
        {
            Left = left;
            IsLeft = true;
        }

        private Either(TRight right)
        {
            Right = right;
            IsLeft = false;
        }

        public static Either<TLeft, TRight> FromLeft(TLeft left) => new(left);
        public static Either<TLeft, TRight> FromRight(TRight right) => new(right);
    }

}
