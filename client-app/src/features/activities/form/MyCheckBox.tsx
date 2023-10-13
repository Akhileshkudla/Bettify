import {  useField } from "formik";
import { Checkbox, Form, Label } from "semantic-ui-react";

interface Props{
    placeholder: string;
    name: string;
    label?: string;
    type?: string;
}

export default function MyCheckBox(props: Props) {
    const [field, meta, helpers] = useField(props.name);

    return (
        <Form.Field error={meta.touched && !!meta.error}>
        <Checkbox toggle
            label={props.label}
            checked={field.value}
            onChange={() => helpers.setValue(!field.value)}
            onBlur={() => helpers.setTouched(true)}
        />
        {meta.touched && meta.error ? (
            <Label basic color="red">
                {meta.error}
            </Label>
        ) : null}
        </Form.Field>
    );
}