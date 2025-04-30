def load_config(file_path):
    import json
    with open(file_path, 'r') as f:
        return json.load(f)

def log_message(message, level='INFO'):
    import logging
    logging.basicConfig(level=logging.INFO)
    logger = logging.getLogger(__name__)
    if level == 'ERROR':
        logger.error(message)
    else:
        logger.info(message)

def validate_data(data, schema):
    from jsonschema import validate, ValidationError
    try:
        validate(instance=data, schema=schema)
        return True
    except ValidationError as e:
        log_message(f"Data validation error: {e.message}", level='ERROR')
        return False