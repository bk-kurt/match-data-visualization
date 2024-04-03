import json

def format_json(input_file, output_file):
   
    with open(input_file, 'r') as infile:
        lines = infile.readlines()


    json_objects = []

  
    for line in lines:
           line = line.strip()
        if line:
          
            json_object = json.loads(line)
            json_objects.append(json_object)

    with open(output_file, 'w') as outfile:
        json.dump(json_objects, outfile, indent=4)

format_json('Applicant-test.json', 'Applicant-test-1.json')
