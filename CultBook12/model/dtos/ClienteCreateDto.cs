namespace CultBook12.model.dtos;

public record ClienteCreateDto(
    string Nome,
    string Login,
    string? Senha,
    string? Email,
    string? Fone
);
