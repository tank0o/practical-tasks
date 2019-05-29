SELECT 
	DISTINCT maker 
FROM 
	public."Product" 
WHERE 
	model ->> 'model' IN 
		(SELECT model ->> 'model' 
		FROM public."Product" 
		WHERE 
			CAST(model ->> 'ram' AS INTEGER) =
		 		(SELECT MIN(CAST(model ->> 'ram' AS INTEGER))   		 
				FROM public."Product"
				WHERE type = 'PC') 
			AND
			type = 'PC') 
		AND 
			CAST(model ->> 'speed' AS INTEGER) = 
				(SELECT MAX(CAST(model ->> 'speed' AS INTEGER)) 
			 	FROM public."Product" 
			 	WHERE 
					CAST(model ->> 'ram' AS INTEGER) = 
			 			(SELECT MIN(CAST(model ->> 'ram' AS INTEGER))
					 	FROM public."Product"
						WHERE type = 'PC') 
					AND
			 		type = 'PC') 
	AND maker IN 
		(SELECT maker
		 FROM public."Product"
		 WHERE type='Printer')