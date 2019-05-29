INSERT INTO public."Product"
	SELECT
	MAX(maker) AS maker,
	json_build_object(
	'code', (MIN(CAST (model ->> 'code' AS INTEGER)) + 20),
	'model', (MAX(CAST(model ->> 'model' AS INTEGER)) + 1000),
	'speed', (MAX(CAST (model ->> 'speed' AS INTEGER))),
	'ram', (MAX(CAST (model ->> 'ram' AS INTEGER)) * 2),
	'hd', (MAX(CAST (model ->> 'hd' AS REAL)) * 2),
	'cd', (SELECT MAX(model ->> 'cd') FROM public."Product"),
	'price', (MAX(CAST (model ->> 'price' AS REAL)) / 1.5)) AS model,
	MAX(type)
	FROM 
		public."Product"
	WHERE 
		type = 'Laptop'
	GROUP BY
		model ->> 'model'