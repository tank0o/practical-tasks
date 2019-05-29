SELECT DISTINCT (model ->> 'model') AS model, (model ->> 'price') AS price
	FROM public."Product"
	WHERE maker = 'B'