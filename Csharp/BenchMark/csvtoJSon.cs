static string CsvToJson(string inputString)
        {
            StringReader reader = new StringReader(inputString);
            var header = reader.ReadLine().Split(',');
            var sb = new StringBuilder();
            sb.AppendLine("[");
            var line = reader.ReadLine();
            do
            {
                if (string.IsNullOrEmpty(line)) break;
                sb.AppendLine("{");
                var colValues = line.Split(',');
                bool moreCols = false;
                int i = 0;
                do
                {
                    sb.AppendFormat("\"{0}\":\"{1}\"", header[i], colValues[i], Environment.NewLine);
                    i++;
                    moreCols = i < header.Length && i < colValues.Length;
                    if (moreCols)
                        sb.AppendFormat(",{0}", Environment.NewLine);

                } while (moreCols);

                sb.Append("\n}");
                line = reader.ReadLine();
                if (!string.IsNullOrEmpty(line))
                    sb.AppendFormat(",{0}", Environment.NewLine);
            } while (true);

            sb.AppendFormat("{0}]", Environment.NewLine);
            return sb.ToString();
        }
        static string s_csv = "name,address,phone \n Romeo,1 miame st,111-344-4567 \n Dhirendra,31 beech st,234-456-4567 \n Azure,3 jfk blvd st \n noName";
