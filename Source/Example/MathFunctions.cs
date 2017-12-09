namespace Example {
    public static class MathFunctions {
        public static double Add(double[] inputs) {
            double sum = 0;
            foreach (double input in inputs)
                sum += input;
            return sum;
        }

        public static double Multiply(double[] inputs) {
            double product = 1;
            foreach (double input in inputs)
                product *= input;
            return product;
        }

        public static double Subtract(double[] inputs) {
            double difference = inputs[0];
            for (int i = 1; i < inputs.Length; ++i)
                difference -= inputs[i];
            return difference;
        }

        public static double Divide(double[] inputs) {
            double quotient = inputs[0];
            for (int i = 1; i < inputs.Length; ++i)
                quotient -= inputs[i];
            return quotient;
        }
    }
}